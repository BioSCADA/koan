package  src
{
	import flash.display.DisplayObjectContainer;
	import flash.errors.IOError;
	import flash.events.DataEvent;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.net.Socket;
	import flash.system.Security;
 	
	public class KoanListener
	{
		public static const RR:String = "RR";
		public static const CO:String = "CO";
		public static const RAN:String = "RAN";
		
		public static function get CONNECTED():Boolean { return SOCKET && SOCKET.connected };
		public static var LISTENING:Boolean = false;
		
		private static var HOST:String;
		private static var PORT:Number;
		
		private static var SOCKET:Socket;
		private static var APP:DisplayObjectContainer;
		
		private static var REQUESTS_QUEUE:Array;
		
		public static function initialize(app:DisplayObjectContainer,host:String,port:int):void
		{
			APP = app;			
			HOST = host;
			PORT = port;
			
			SOCKET = new Socket();
			SOCKET.addEventListener(Event.CLOSE, onSocketClosed);
			SOCKET.addEventListener(Event.CONNECT, onSocketConnected);
			SOCKET.addEventListener(ProgressEvent.SOCKET_DATA, onSocketData);
			SOCKET.addEventListener(IOErrorEvent.IO_ERROR, onSocketError);
			
			REQUESTS_QUEUE = new Array();
			
			connect();
		}
		
		public static function connect():void
		{
			//Security.allowDomain("*");
			if(!SOCKET.connected)
				SOCKET.connect(HOST,PORT);
		}
		
		public static function disconnect():void
		{
			SOCKET.close();
		}
		
		private static function onSocketClosed(e:Event):void
		{
			APP.dispatchEvent(new KoanListenerEvent(KoanListenerEvent.DISCONNECTED));
		}
		
		private static function onSocketError(e:IOErrorEvent):void
		{
			APP.dispatchEvent(new KoanListenerEvent(KoanListenerEvent.CONNECTION_ERROR));
		}
		
		private static function onSocketConnected(e:Event):void
		{
			APP.dispatchEvent(new KoanListenerEvent(KoanListenerEvent.CONNECTED));
		}
		
		private static function onSocketData(e:ProgressEvent):void 
		{ 
			var response:String = "0"; 
			while (SOCKET.bytesAvailable) 
				response += SOCKET.readUTFBytes(SOCKET.bytesAvailable);
						
			var value:String = String(response);
			
			switch(REQUESTS_QUEUE[0])
			{
				case RR:{ APP.dispatchEvent(new KoanEvent(KoanEvent.RR_ACQUIRED,value)); nextReq(); } break;
				case CO:{ APP.dispatchEvent(new KoanEvent(KoanEvent.CO_ACQUIRED,value)); nextReq(); } break;
				case RAN:{ APP.dispatchEvent(new KoanEvent(KoanEvent.RAN_ACQUIRED,value)); nextReq(); } break;
			}
			
			function nextReq():void
			{
				REQUESTS_QUEUE.splice(0,1);
				if(REQUESTS_QUEUE.length > 0)
					send(REQUESTS_QUEUE[0].toString());
			}			
		}
		
		public static function acquireRR():void
		{
			REQUESTS_QUEUE.push(RR);
			if(REQUESTS_QUEUE.length == 1)
				send(REQUESTS_QUEUE[0].toString());
		}
		
		public static function acquireCO():void
		{
			REQUESTS_QUEUE.push(CO);
			if(REQUESTS_QUEUE.length == 1)
				send(REQUESTS_QUEUE[0].toString());
		}
		
		public static function acquireRAN():void
		{
			REQUESTS_QUEUE.push(RAN);
			if(REQUESTS_QUEUE.length == 1)
				send(REQUESTS_QUEUE[0].toString());
		}
		
		public static function send(data:Object):void
		{
			try { 
				SOCKET.writeUTFBytes(data.toString());
				SOCKET.writeUTFBytes("\r\n"); 
				SOCKET.flush();
			}
			catch(e:IOError) {
				APP.dispatchEvent(new KoanListenerEvent(KoanListenerEvent.CONNECTION_ERROR));
			}
		}
	}
}