/*
 * Part of BioSCADA Library
 *
 * Copyright (c) 2012, Diego Schmaedech (schmaedech@gmail.com)
 * Dual-licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and the Beerware (http://en.wikipedia.org/wiki/Beerware) license.
 */
 package src
{

	public class DataControl
	{

		public static var login:String = "";
		public static var senha:String = "";
        
		  
		private static var instance:DataControl = new DataControl();

		public function DataControl()
		{
			if (instance)
			{
				throw new Error("We cannot create a new instance. Please use DataControl.getInstance()");
			}
		}

		public static function getInstance():DataControl
		{
			return instance;
		}
	}

}