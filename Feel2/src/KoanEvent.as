package  src
{
	import flash.events.Event;

	public class KoanEvent extends Event
	{
		public var value:String;
		
		public static const RR_ACQUIRED:String = "RR_ACQUIRED";
		public static const CO_ACQUIRED:String = "CO_ACQUIRED";
		public static const RAN_ACQUIRED:String = "RAN_ACQUIRED";
		
		public function KoanEvent(type:String, value:String = "", bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.value = value;
		}
	}
}