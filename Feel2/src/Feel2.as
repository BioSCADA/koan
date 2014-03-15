/*
 * Part of BioSCADA Library
 *
 * Copyright (c) 2012, Diego Schmaedech (schmaedech@gmail.com)
 * Dual-licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and the Beerware (http://en.wikipedia.org/wiki/Beerware) license.
 */

package src
{

	import flash.display.MovieClip;
	import flash.system.*;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import flash.events.*;
	import flash.display.*;
	import flash.net.URLRequest;
	import flash.media.Sound;
	import flash.events.Event;
	import flash.media.SoundChannel;
	import flash.errors.*;
	import fl.transitions.*;
	import fl.transitions.easing.*;
	import flash.utils.*;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.filters.GlowFilter;
	import flash.filters.BitmapFilterQuality;
	import fl.controls.Slider;
	import fl.events.SliderEvent;
	import flash.globalization.NumberFormatter;
	import flash.globalization.LocaleID;
	
	public class Feel2 extends MovieClip
	{
		trace("copy right @ Diego Schmaedech");

		var lastRRValue:Number = Number(0);
		var lastCOValue:Number = Number(0);
		var lastRANValue:Number = Number(0);

		var pvCenario:Number = 0;//Variável que armazena a posição inicial do cenário;
		var avCenario:Number = 1;//Variável que armazena a aceleração variável do cenário;
		var aWing:Number = 1;//Variável que armazana a aceleração da Asa Delta;
		var pWing:Number = 0;//Variável que armazena a posição inicial da Asa;
		//Partes que integram o cenário;
		var _part1:Part1 = new Part1();
		var _part2:Part2 = new Part2();
		var _part3:Part3 = new Part3();
		var _part4:Part4 = new Part4();
		var _part5:Part5 = new Part5();
		var begin:Begin= new Begin();//Tela inicial
		var _levels:Levels = new Levels();//Cenário de fundo;
		var _wing:ParaG = new ParaG();//Asa Delta
		var _game_over:GameOver = new GameOver();//Asa Delta

		var _pausa:Pausa = new Pausa();//Variáveis de tempo corrente;
		var _restart:BtRestart = new BtRestart();//Variáveis de tempo corrente;



		var _bitmap:BitmapData;

		var _w:Number;
		var _h:Number;//Bg Audio




		var glowColor:Number = 0xff9600;//laranja
		var glowAlpha:Number = .8;
		var glowBlurX:Number = 3;
		var glowBlurY:Number = 3;
		var glowStrength:Number = 2;
		var glowQuality:Number = BitmapFilterQuality.HIGH;
		var glowInner:Boolean = false;
		var glowKnockout:Boolean = false;
		var glowFilter:GlowFilter = new GlowFilter(glowColor,glowAlpha,glowBlurX,glowBlurY,glowStrength,glowQuality,glowInner,glowKnockout);
		var glowFilterRed:GlowFilter = new GlowFilter(0xed252f,glowAlpha,glowBlurX,glowBlurY,glowStrength,glowQuality,glowInner,glowKnockout);
		var glowFilterGreen:GlowFilter = new GlowFilter(0x006d00,glowAlpha,glowBlurX,glowBlurY,glowStrength,glowQuality,glowInner,glowKnockout);
		var glowFilterBlue:GlowFilter = new GlowFilter(0x429ddc,glowAlpha,glowBlurX,glowBlurY,glowStrength,glowQuality,glowInner,glowKnockout);

		var glowFilterWhite:GlowFilter = new GlowFilter(0xffffff,glowAlpha,glowBlurX,glowBlurY,glowStrength,glowQuality,glowInner,glowKnockout);

		function fl_MouseOverHandler(event:MouseEvent):void
		{
			event.currentTarget.filters = [glowFilter];

			event.currentTarget.addEventListener(MouseEvent.MOUSE_OUT,onMouseOut);

			function onMouseOut(event:Event)
			{

				event.currentTarget.filters = null;

			}
		}

		//Security.allowDomain("*");
		private function onKoan_RR_Acquired(e:KoanEvent):void
		{
			var RRValue = new Number(e.value);

			if (RRValue < 1500 && RRValue > 300 && lastRRValue !=  RRValue)
			{
				_pausa._btHeartIcon._heart_icon_text.text = Math.round(RRValue);
				lastRRValue = new Number(e.value);
				//trace("RAN_Acquired: "+lastRRValue);
			}
			else
			{

			}


		}

		private function onKoan_CO_Acquired(e:KoanEvent):void
		{
			/*var nf:NumberFormatter = new NumberFormatter( "en-US" ); 
			var inputNumberString:String = e.value; 
			 var parsedNumber:Number = nf.parseNumber(inputNumberString); 
			var COValue = parsedNumber;
			trace(e.value);*/
			 
			var COValue = Number(e.value.split(",").join("."));
trace(COValue);
			if ( lastCOValue !=  COValue)
			{
				if (lastCOValue >=  COValue)
				{
					_wing.x -=  1;
					aWing -=  1;
					//avCenario -=  1;
				}
				else
				{
					_wing.x +=  1;
					aWing +=  1;
					//avCenario +=  1;
				}
				if (COValue >=  1)
				{
					COValue = 1;
					//avCenario -=  1;
				}
				_pausa._btCoIcon._co_icon_text.text = COValue;
				//var myHoriTween:Tween = new Tween(_wing,"y",Back.easeInOut,_wing.y,480 - (460 * lastCOValue),1,true);
				_wing.y = 480 - (460 * lastCOValue);
				//trace("lastCOValue: " + lastCOValue);
				lastCOValue = COValue;
				_processing( );
			}
		}


		public function InitializeKoanGear()
		{
			this.addEventListener(KoanListenerEvent.CONNECTED, onListenerConnected);
			this.addEventListener(KoanListenerEvent.DISCONNECTED, onListenerDisconnected);
			this.addEventListener(KoanListenerEvent.CONNECTION_ERROR, onListenerConnectionError);
			this.addEventListener(KoanEvent.RR_ACQUIRED, onKoan_RR_Acquired);
			this.addEventListener(KoanEvent.CO_ACQUIRED, onKoan_CO_Acquired);

			KoanListener.initialize(this, "localhost", 9999);

			function onListenerConnected(e:KoanListenerEvent):void
			{
				_pausa._TCP.gotoAndStop(1);
				trace("onFeelRioListenerConnected");
			}

			function onListenerDisconnected(e:KoanListenerEvent):void
			{
				setTimeout(function():void{ KoanListener.connect(); },1000);
				trace("onFeelRioListenerDisconnected");
				_pausa._TCP.gotoAndStop(2);
			}

			function onListenerConnectionError(e:KoanListenerEvent):void
			{
				setTimeout(function():void{ KoanListener.connect(); },1000);
				_pausa._TCP.gotoAndStop(2);
				trace("onFeelRioListenerConnectionError");
			}
		}



		function keyPressedDown(event:KeyboardEvent):void
		{
			var key:uint = event.keyCode;
			var step1:Number = 1;
			var step2:Number = 1;
			switch (key)
			{
				case Keyboard.LEFT :
					_wing.x -=  step1;
					aWing -=  step2;
					//trace( "_wing.x -=  step*5" );
					break;
				case Keyboard.RIGHT :
					_wing.x +=  step1;
					aWing +=  step2;
					//trace( "_wing.x +=  step*5" );
					break;
				case Keyboard.UP :
					//stage.addEventListener( Event.ENTER_FRAME, _aCenario );
					_wing.y -=  step1;
					//avCenario +=  step2;
					//trace( "_wing.y -=  step*5" );
					break;
				case Keyboard.DOWN :
					//stage.removeEventListener( Event.ENTER_FRAME, _aCenario );
					_wing.y +=  step1;
					//avCenario -=  step2;
					//trace( "_wing.y +=  step*5" );
					break;
			}
		}

		function _accelerate(inc:Number):void
		{
			avCenario +=  inc;
		}
		function _desaccelerate(inc:Number):void
		{
			avCenario -=  inc;
		}
		function _windUp(inc:Number):void
		{
			_wing.y +=  inc;
		}
		function _windDown(inc:Number):void
		{
			_wing.y -=  inc;
		}
		function _windGo(inc:Number):void
		{
			_wing.x +=  inc;
		}
		function _windBack(inc:Number):void
		{
			_wing.x -=  inc;
		}

		function _begin():void
		{
			addChild( begin );
			begin.buttonMode = true;
			begin.addEventListener( MouseEvent.MOUSE_DOWN, _beginEnd );

		}
		function _beginEnd(e:MouseEvent):void
		{
			_startGame();

		}
		function _startGame():void
		{

			_wing.play();

			addChild( _levels );
			addChild( _wing );
			addChild( _pausa );
			addChild( _restart);
			addChild( _game_over);
			_game_over.visible = false;
			_game_over.buttonMode = true;

			_restart.buttonMode = true;
			_pausa._bgPausa.alpha = 0;
			_pausa._coerencia.filters = [glowFilterWhite];
			_pausa._TCP.filters = [glowFilterBlue];
			_pausa._time.filters = [glowFilter];
			_pausa.timer.start();

			_pausa._bt_play.visible = false;
			_restart.addEventListener(MouseEvent.MOUSE_OVER, fl_MouseOverHandler);
			_pausa._btHeartIcon.addEventListener(MouseEvent.MOUSE_OVER, fl_MouseOverHandler);
			_pausa._btPausa.addEventListener(MouseEvent.MOUSE_OVER, fl_MouseOverHandler);


			_wing.y = 200;
			_wing.x = 100;
			_wing.alpha = 1;
			pWing = 0;
			aWing = 0;
			_wing.x = pWing +=  aWing;
			aWing +=  300;

			_levels.alpha = 1;
			_levels.addChildAt( _part1, 0);

			//_bottom._bgBottom.width = 1024 + 10;

			//_bottom._velocidade.x = 13;
			//_pausa._coerencia.x = 1024 - _pausa._coerencia.width - 50;
			_pausa._coerencia._bRed.alpha = .4;
			_pausa._coerencia._bBlue.alpha = .4;
			_pausa._coerencia._bGreen.alpha = .4;


			stage.addEventListener( Event.ENTER_FRAME, _aCenario );
			stage.addEventListener( Event.ENTER_FRAME, _aWing );
			stage.addEventListener( KeyboardEvent.KEY_DOWN, keyPressedDown);


			_pausa._btPausa.addEventListener( MouseEvent.MOUSE_DOWN, _pause );


			_restart.addEventListener( MouseEvent.MOUSE_DOWN, _restartGame );
			_game_over.addEventListener( MouseEvent.MOUSE_DOWN, _restartGame );
			_pausa._bt_levels.addEventListener(SliderEvent.CHANGE, announceChange);


		}


		function announceChange(e:SliderEvent):void
		{
			//trace("Slider value is now: " + );
			avCenario = e.target.value;
			//trace("Slider value is now: " + avCenario);
		}

		function _aCenario( e:Event ):void
		{
			_levels.x = pvCenario -=  avCenario;

			if (_levels.x >= 0)
			{

				_levels.x = 0;
				// trace("_levels.x: "+0);
				stage.removeEventListener(Event.ENTER_FRAME, _aCenario);

			}
			if (_levels.x <= -2100)
			{
				// trace("_levels.x: "+2100);
				_levels.addChildAt( _part2,1 );
				_part2.x = _part1.width;
			}
			if (_levels.x <= -4200)
			{
				//trace("_levels.x: "+6300);
				_levels.addChildAt( _part3,1 );
				_part3.x = _part1.width + _part2.width;//_levels.removeChild( _part1 );
			}
			if (_levels.x <= -6300)
			{
				//trace("_levels.x: "+6300);
				_levels.addChildAt( _part4 , 1);
				_part4.x = _part1.width + _part2.width + _part3.width;//removeChild( _part2 );
			}
			if (_levels.x <= -8400)
			{
				//trace("_levels.x: "+8400);
				_levels.addChildAt( _part5, 1 );
				_part5.x = _part1.width + _part2.width + _part3.width + _part4.width;//removeChild( _part3 );
			}
			if (_levels.x <= -14100)
			{
				_game_over.visible = true;

				var moveTween2:Tween = new Tween(_game_over,"alpha",Elastic.easeOut,.5,1,1,true);


				_wing.stop();
				_levels.x = -14100;

				// stage.removeEventListener(Event.ENTER_FRAME, _aCenario);
			}

			//trace("_levels.x: "+_levels.x);
		}
		function _aWing( e:Event ):void
		{
			_wing.x = pWing +=  aWing;
			//_wing.x = pWing;

			stage.removeEventListener(Event.ENTER_FRAME, _aWing);
		}

		function _processing( ):void
		{


			var desired_x:Number =  -  _levels.x + _wing.x;
			var desired_y:Number = _wing.y;
			var desired_z:Number = -10;
			var desiredColour:uint = 0xFF00FF00;
			var pointGraphicData = new BitmapData(3,3,true,desiredColour);
			var pointGraphic:Bitmap = new Bitmap(pointGraphicData);
			//primeiro 1/3
			if (_wing.y > 0 && _wing.y < 480 / 3)
			{
				desiredColour = 0x00958a;
				_pausa._time.filters = [glowFilterGreen];

				pointGraphicData = new BitmapData(3,3,true,desiredColour);
				pointGraphic = new Bitmap(pointGraphicData);

				pointGraphic.x = desired_x;
				pointGraphic.y = desired_y;
				pointGraphic.z = desired_z;

				// _levels.addChild(pointGraphic );
				var _btGreen:BtGreen = new BtGreen();
				_btGreen.x = desired_x;
				_btGreen.y = desired_y;
				_btGreen.z = desired_z;
				_btGreen.scaleX = .5;
				_btGreen.scaleY = .5;

				_levels.addChild(_btGreen );

				_pausa._coerencia._bRed.alpha = .4;
				_pausa._coerencia._bBlue.alpha = .4;
				_pausa._coerencia._bGreen.alpha = 1;
			}
			//segundo 1/3
			if (_wing.y > 480 / 3 && _wing.y < 2 * (480 / 3))
			{
				desiredColour = 0x429ddc;
				_pausa._time.filters = [glowFilterBlue];

				pointGraphicData = new BitmapData(3,3,true,desiredColour);
				pointGraphic = new Bitmap(pointGraphicData);

				pointGraphic.x = desired_x;
				pointGraphic.y = desired_y;
				pointGraphic.z = desired_z;

				//_levels.addChild(pointGraphic );
				//Asa Delta
				var _btBlue:BtBlue = new BtBlue();
				_btBlue.x = desired_x;
				_btBlue.y = desired_y;
				_btBlue.z = desired_z;
				_btBlue.scaleX = .5;
				_btBlue.scaleY = .5;

				_levels.addChild(_btBlue );

				_pausa._coerencia._bRed.alpha = .4;
				_pausa._coerencia._bBlue.alpha = 1;
				_pausa._coerencia._bGreen.alpha = .4;
			}

			if (_wing.y > 2 * (480 / 3) && _wing.y < 480)
			{
				desiredColour = 0xff222e;

				_pausa._time.filters = [glowFilterRed];
				pointGraphicData = new BitmapData(3,3,true,desiredColour);
				pointGraphic = new Bitmap(pointGraphicData);

				pointGraphic.x = desired_x;
				pointGraphic.y = desired_y;
				pointGraphic.z = desired_z;

				//_levels.addChild(pointGraphic );
				var _btRed:BtRed = new BtRed();//Asa Delta
				_btRed.x = desired_x;
				_btRed.y = desired_y;
				_btRed.z = desired_z;
				_btRed.scaleX = .5;
				_btRed.scaleY = .5;

				_levels.addChild(_btRed );

				_pausa._coerencia._bRed.alpha = 1;
				_pausa._coerencia._bBlue.alpha = .4;
				_pausa._coerencia._bGreen.alpha = .4;
			}



		}

		function _pause( e:MouseEvent )
		{
			if (_pausa._bgPausa.alpha == 1)
			{
				if (_levels.x <= -14100)
				{
					_pausa._bt_play.visible = true;
					_pausa._bt_play.addEventListener( MouseEvent.MOUSE_DOWN, _pause );
					_pausa._bgPausa.gotoAndStop(1);

					var moveTween6:Tween = new Tween(_pausa._bgPausa,"alpha",Elastic.easeOut,0,1,1,true);
					// _pausa._bgPausa._transiction_action.text = "Re-Play \o/";
					stage.removeEventListener(Event.ENTER_FRAME, _aCenario);
					// stage.removeEventListener(Event.ENTER_FRAME, _aWing);
					_wing.stop();
					_pausa.timer.stop();
					_restartGame( e  );

					// stage.removeEventListener(Event.ENTER_FRAME, _aCenario);
				}
				else
				{

					_pausa._bt_play.visible = false;
					_pausa._bt_play.removeEventListener( MouseEvent.MOUSE_DOWN, _pause );
					//  _pausa._btPausa.alpha = 0;

					var moveTween1:Tween = new Tween(_pausa._bgPausa,"alpha",Elastic.easeOut,1,0,1,true);
					//_pausa._bgPausa.alpha = 0;
					//_pausa.clear();
					stage.addEventListener(Event.ENTER_FRAME, _aCenario);
					//stage.addEventListener(Event.ENTER_FRAME, _aWing);
					_pausa.timer.start();
					_wing.play();
				}
			}
			else
			{
				_pausa._bt_play.visible = true;
				_pausa._bt_play.addEventListener( MouseEvent.MOUSE_DOWN, _pause );
				_pausa._bgPausa.gotoAndStop(1);

				var moveTween2:Tween = new Tween(_pausa._bgPausa,"alpha",Elastic.easeOut,0,1,1,true);
				// _pausa._bgPausa._transiction_action.text = "Re-Play \o/";
				stage.removeEventListener(Event.ENTER_FRAME, _aCenario);
				// stage.removeEventListener(Event.ENTER_FRAME, _aWing);
				_wing.stop();
				_pausa.timer.stop();

			}
		}
		function _restartGame( e:MouseEvent )
		{

			while ( numChildren > 0)
			{
				removeChildAt(0);
			}
			while ( _levels.numChildren > 0)
			{
				_levels.removeChildAt(0);
			}

			lastRRValue = Number(0);
			lastCOValue = Number(0);
			lastRANValue = Number(0);

			pvCenario = 0;//Variável que armazena a posição inicial do cenário;
			avCenario = 1;//Variável que armazena a aceleração variável do cenário;
			aWing = 1;//Variável que armazana a aceleração da Asa Delta;
			pWing = 0;//Variável que armazena a posição inicial da Asa;
			//Partes que integram o cenário;
			_part1  = new Part1();
			_part2  = new Part2();
			_part3  = new Part3();
			_part4  = new Part4();
			_part5  = new Part5();
			begin  = new Begin();//Tela inicial
			_levels  = new Levels();//Cenário de fundo;
			_game_over  = new GameOver();//Asa Delta

			_pausa = new Pausa();//Variáveis de tempo corrente;
			_restart  = new BtRestart();//Variáveis de tempo corrente;


			stage.removeEventListener(Event.ENTER_FRAME, _aCenario);
			stage.removeEventListener(Event.ENTER_FRAME, _aWing);

			_wing.stop();
			_begin();
		}
		function acquire():void
		{
			//KoanListener.acquireRAN();
			KoanListener.acquireRR();
			KoanListener.acquireCO();

			setTimeout(acquire,300);//nova aquisição em 100ms = -> "looping" (while true...)
		}

		public function Feel2()
		{

			_begin();
			InitializeKoanGear();

			acquire();

		}
	}

}