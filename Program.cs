using HistoryGenerator.Collections;
using HistoryGenerator.Generators;
using HistoryGenerator.Model;
using System;
using System.Windows.Forms;

namespace HistoryGenerator
{
	public class Program
	{
		public static World World;
		public static WorldSettings WorldSettings;
		public static HeightMapGenerator HeightMapGenerator;
		public static ClimateMapGenerator ClimateMapGenerator;
		public static WaterMapGenerator WaterMapGenerator;
		
		public static event EventHandler Regenerated;

		[STAThread]
		static void Main()
		{
			WorldSettings = new WorldSettings
			{
				Width = 800,
				Height = 800
			};

			HeightMapGenerator = new HeightMapGenerator
			{
				Settings = new HeightMapGeneratorSettings
				{
					Seed = 0,
					Scale = 150,
					Octaves = 5,
					Persistance = 0.60,
					Lacunarity = 1.75,
					Steps = 0
				}
			};

			WaterMapGenerator = new WaterMapGenerator
			{
				Settings = new WaterMapGeneratorSettings
				{
					Seed = 0,
					SeaLevel = 0.4,
					MinSpringLevel = 0.5,
					MaxSpringCount = 50,
					MaxPoolSize = 10
				}
			};

			ClimateMapGenerator = new ClimateMapGenerator()
			{
				Settings = new ClimateMapGeneratorSettings
				{
					Seed = 0,
					Scale = 100
				}
			};

			HistoryGeneratorWindow window = BuildWindow();

			Application.EnableVisualStyles();
			Application.Run(window);
		}

		public static void Regenerate()
		{
			World = new World(WorldSettings.Width, WorldSettings.Height);

			Map<double> heightMap = HeightMapGenerator.Generate();
			World.AddMap("HeightMap", heightMap);

			Map<WaterType> waterMap = WaterMapGenerator.Generate();
			World.AddMap("WaterMap", waterMap);

			Map<ClimateData> climateMap = ClimateMapGenerator.Generate();
			World.AddMap("ClimateMap", climateMap);

			Regenerated?.Invoke(null, EventArgs.Empty);
		}

		private static HistoryGeneratorWindow BuildWindow()
		{
			HistoryGeneratorWindow window = new HistoryGeneratorWindow();

			IntPtr tabHandle = window.AddTab("World");
			
			IntPtr groupHandle1 = window.AddGroup(tabHandle, "World");
			window.AddNumberSetting(groupHandle1, "Width", WorldSettings, nameof(WorldSettings.Width), 0, 5000, 50);
			window.AddNumberSetting(groupHandle1, "Height", WorldSettings, nameof(WorldSettings.Height), 0, 5000, 50);

			IntPtr groupHandle2 = window.AddGroup(tabHandle, "HeightMap");
			window.AddNumberSetting(groupHandle2, "Seed", HeightMapGenerator.Settings, nameof(HeightMapGeneratorSettings.Seed), int.MinValue, int.MaxValue);
			window.AddNumberSetting(groupHandle2, "Scale", HeightMapGenerator.Settings, nameof(HeightMapGeneratorSettings.Scale), 0.01, 5000, 10, 2);
			window.AddNumberSetting(groupHandle2, "Octaves", HeightMapGenerator.Settings, nameof(HeightMapGeneratorSettings.Octaves), 1, 10, 1);
			window.AddNumberSetting(groupHandle2, "Persistance", HeightMapGenerator.Settings, nameof(HeightMapGeneratorSettings.Persistance), 0, 5, 0.1, 2);
			window.AddNumberSetting(groupHandle2, "Lacunarity", HeightMapGenerator.Settings, nameof(HeightMapGeneratorSettings.Lacunarity), 0, 5, 0.1, 2);
			window.AddNumberSetting(groupHandle2, "Steps", HeightMapGenerator.Settings, nameof(HeightMapGeneratorSettings.Steps), 0, 20);

			IntPtr groupHandle3 = window.AddGroup(tabHandle, "WaterMap");
			window.AddNumberSetting(groupHandle3, "Seed", WaterMapGenerator.Settings, nameof(WaterMapGeneratorSettings.Seed), int.MinValue, int.MaxValue);
			window.AddNumberSetting(groupHandle3, "SeaLevel", WaterMapGenerator.Settings, nameof(WaterMapGeneratorSettings.SeaLevel), 0, 1, 0.05, 2);
			window.AddNumberSetting(groupHandle3, "MinSpringLevel", WaterMapGenerator.Settings, nameof(WaterMapGeneratorSettings.MinSpringLevel), 0, 1, 0.05, 2);
			window.AddNumberSetting(groupHandle3, "MaxSpringCount", WaterMapGenerator.Settings, nameof(WaterMapGeneratorSettings.MaxSpringCount), 0, 50, 1);
			window.AddNumberSetting(groupHandle3, "MaxPoolSize", WaterMapGenerator.Settings, nameof(WaterMapGeneratorSettings.MaxPoolSize), 0, 30, 1);

			IntPtr groupHandle4 = window.AddGroup(tabHandle, "ClimateMap");
			window.AddNumberSetting(groupHandle4, "Seed", ClimateMapGenerator.Settings, nameof(ClimateMapGeneratorSettings.Seed), int.MinValue, int.MaxValue);
			window.AddNumberSetting(groupHandle4, "Scale", ClimateMapGenerator.Settings, nameof(ClimateMapGeneratorSettings.Scale), 0.01, 5000, 10, 2);

			return window;
		}

		public const string SettingsFileExt = ".json";
		public static void SaveSettings(string filePath)
		{
		}

		public static void LoadSettings(string filePath)
		{
		}
	}
}
