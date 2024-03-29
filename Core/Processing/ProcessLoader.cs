﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HistoryGenerator.Core.Processing
{
	public class ProcessAttribute : Attribute
	{
		public string ProcessChain { get; set; } = "Default";
		public Type[] Dependencies { get; set; } = new Type[0];
	}

	public static class ProcessLoader
	{
		public static readonly ReadOnlyDictionary<string, ProcessChain> LoadedProcessChains;
		public static readonly ReadOnlyCollection<Process> LoadedProcesses;

		private static readonly Dictionary<string, ProcessChain> _loadedProcessChains = new Dictionary<string, ProcessChain>();
		private static readonly List<Process> _loadedProcesses = new List<Process>();

		static ProcessLoader()
		{
			IEnumerable<Type> processTypes = typeof(Program).Assembly
				.GetTypes()
				.Where(t => t.GetCustomAttribute<ProcessAttribute>() != null);

			foreach (Type t in processTypes)
			{
				LoadProcess(t);
			}

			LoadedProcessChains = new ReadOnlyDictionary<string, ProcessChain>(_loadedProcessChains);
			LoadedProcesses = new ReadOnlyCollection<Process>(_loadedProcesses);
		}

		private static void LoadProcess(Type processType, List<Type> seenTypes = null)
		{
			if (_loadedProcesses.Any(p => p.GetType() == processType)) return;

			if (seenTypes == null)
			{
				seenTypes = new List<Type>();
			}
			else if (seenTypes.Contains(processType))
			{
				throw new Exception($"Failed to load '{processType.FullName}'. Circular dependency detected.");
			}

			seenTypes.Add(processType);

			ProcessAttribute processAttribute = processType.GetCustomAttribute<ProcessAttribute>();
			IEnumerable<Type> unloadedDependencies = processAttribute.Dependencies.Except(_loadedProcesses.Select(p => p.GetType()));
			foreach (Type t in unloadedDependencies)
			{
				LoadProcess(t, seenTypes);
			}

			string processChainTag = processAttribute.ProcessChain;
			if (!_loadedProcessChains.ContainsKey(processChainTag))
			{
				_loadedProcessChains.Add(processChainTag, new ProcessChain(processChainTag));
			}

			Process process = (Process)Activator.CreateInstance(processType);
			_loadedProcessChains[processAttribute.ProcessChain].Add(process);
			_loadedProcesses.Add(process);
		}

		public static T GetLoadedProcess<T>() where T : Process
		{
			T process = (T)LoadedProcesses.FirstOrDefault(process => process is T);
			return process ?? throw new Exception($"Failed to get loaded process '{typeof(T).FullName}'.");
		}
	}
}
