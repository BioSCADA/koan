/*
 * Part of BioSCADA Library
 *
 * Copyright (c) 2012, Diego Schmaedech (schmaedech@gmail.com)
 * Dual-licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and the Beerware (http://en.wikipedia.org/wiki/Beerware) license.
 */
 package src
{

	public class DataLogger
	{

		public static var StartTimestamp:int = new int();
		public static var EndTimestamp:int = new int();
		public static var StartDate:String = new String();
		public static var EndDate:String = new String();
		public static var RefTimestamp:String = new String();
		public static var DiffTimestamp:int = new int();
		public static var timeline:int = new int(); 
		 
		public static var idFromPHP:String = new String(); 
		   
		
		private static var instance:DataLogger = new DataLogger(); 
		
		public function DataLogger()
		{
			if(instance)
			{
				throw new Error ("We cannot create a new instance. Please use DataControl.getInstance()");
			}
		}
 
		public static function getInstance():DataLogger
		{
			return instance;
		}
	}
}