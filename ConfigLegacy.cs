﻿
using System;
using System.IO;
using Terraria;

namespace BossExpertise
{
	public static class ConfigLegacy
	{
		static string ConfigFolderPath = Path.Combine(Main.SavePath, "Mod Configs", "BossExpertise");
		static string ConfigPath = Path.Combine(ConfigFolderPath, "config.txt");
		static string ConfigVersionPath = Path.Combine(ConfigFolderPath, "configVersion.txt");

		public static bool Load()
		{
			if(!Directory.Exists(ConfigFolderPath))
				return false;

			bool success = false;

			if(File.Exists(ConfigPath))
			{
				BossExpertise.Instance.Logger.Warn("Found config file with old format! Reading outdated config...");
				var file = new StreamReader(ConfigPath);
				try
				{
					file.ReadLine();
					bool.TryParse(file.ReadLine().Split(':')[1], out Config.DropBags);

					file.ReadLine();
					bool.TryParse(file.ReadLine().Split(':')[1], out Config.ChangeBossAI);

					file.ReadLine();
					bool.TryParse(file.ReadLine().Split(':')[1], out Config.AddCheatSheetButton);

					file.ReadLine();
					bool.TryParse(file.ReadLine().Split(':')[1], out Config.AddExpertCommand);

					success = true;
				}
				catch(Exception e)
				{
					BossExpertise.Instance.Logger.Error("Unable to read old config file!", e);
				}
				finally
				{
					file.Dispose();
				}
			}

			BossExpertise.Instance.Logger.Warn("Deleting outdated config...");
			try
			{
				File.Delete(ConfigPath);
				File.Delete(ConfigVersionPath);
				if(Directory.GetFiles(ConfigFolderPath).Length == 0 && Directory.GetDirectories(ConfigFolderPath).Length == 0)
				{
					Directory.Delete(ConfigFolderPath);
				}
				else
				{
					BossExpertise.Instance.Logger.Warn("Outdated config folder still cotains some files/directories. They will not get deleted.");
				}
			}
			catch(Exception e)
			{
				BossExpertise.Instance.Logger.Error("Unable to delete old config!", e);
			}
			return success;
		}
	}
}
