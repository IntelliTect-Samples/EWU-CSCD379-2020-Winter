using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
	public static class SampleData
	{
		static public Gift CreateViper() => new Gift("Viper", "Fast spaceship", "www.vipers.com", CreateKaraThrace());
		static public Gift CreateCylonDetector() => new Gift("Cylon Detector", "Version 1.1 certified for models 1-5", "www.findacylon.com", CreateGaiusBaltar());
		static public Gift CreateFTLDrive() => new Gift("FTL Drive", "Version 1.0", "www.ftl-drives.com", CreateWilliamAdama());


		static public User CreateWilliamAdama() => new User("William", "Adama");
		static public User CreateLeeAdama() => new User("Lee", "Adama");
		static public User CreateKaraThrace() => new User("Kara", "Thrace");
		static public User CreateGaiusBaltar() => new User("Gaius", "Baltar");

		static public Group CreateColonialFleet() => new Group("Colonial Fleet");
		static public Group CreateCylonShip() => new Group("Cylon Ship");

	}
}
