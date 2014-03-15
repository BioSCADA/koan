package  src
{
	import flash.events.Event;

	public class KoanListenerEvent extends Event
	{
		public static const CONNECTED:String = "koan_connected";
		public static const DISCONNECTED:String = "koan_disconnected";
		public static const CONNECTION_ERROR:String = "koan_connection_error";
		public static const UPDATED:String = "koan_updated";
		
		public function KoanListenerEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
		}
	}
}