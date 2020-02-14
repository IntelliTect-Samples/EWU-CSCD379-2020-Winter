using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
	public static class SampleData
	{
		static public Gift CreateGiftViper() => new Gift("Viper", "www.vipers.com", "Fast spaceship", CreateUserKaraThrace());
		static public Gift CreateGiftCylonDetector() => new Gift("Cylon Detector", "www.findacylon.com", "Version 1.1 certified for models 1-5", CreateUserGaiusBaltar());
		static public Gift CreateGiftFTLDrive() => new Gift("FTL Drive", "www.ftl-drives.com", "Version 1.0", CreateUserWilliamAdama());


		static public User CreateUserWilliamAdama() => new User("William", "Adama");
		static public User CreateUserLeeAdama() => new User("Lee", "Adama");
		static public User CreateUserKaraThrace() => new User("Kara", "Thrace");
		static public User CreateUserGaiusBaltar() => new User("Gaius", "Baltar");
		static public User CreateUserNumber6() => new User("Number", "6");
		static public User CreateUserLauraRoslin() => new User("Laura", "Roslin");
		static public User CreateUserSaulTigh() => new User("Saul", "Tigh");

		static public Group CreateGroupColonialFleet() => new Group("Colonial Fleet");
		static public Group CreateGroupCylonShip() => new Group("Cylon Ship");
	}
}