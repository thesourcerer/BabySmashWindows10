﻿using BabySmash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace BabySmash.Windows.Helpers
{
	internal static class Animations
	{
		public static Storyboard CreateDPAnimation(DependencyObject shape, string dp, Duration duration, double from, double to, bool loop = false, bool autoReverse = false, EasingFunctionBase easing = null, Storyboard st = null)
		{
			if(st == null)
				st = new Storyboard();

			var d = new DoubleAnimation {
				From = from,
				To = to,
				Duration = duration,
				EasingFunction = easing,
				AutoReverse = autoReverse
			};

			if(loop)
				d.RepeatBehavior = RepeatBehavior.Forever;

			st.Children.Add(d);
			Storyboard.SetTarget(d, shape);
			Storyboard.SetTargetProperty(d, dp);
			return st;
		}

		public static void ApplyRandomAnimationEffect(FrameworkElement fe, Duration duration)
		{
			fe.RenderTransformOrigin = new Point(0.5, 0.5);
			var tf = (fe.RenderTransform as TransformGroup);
			if(tf == null)
				fe.RenderTransform = tf = new TransformGroup();
			int e = Utils.RandomBetweenTwoNumbers(0, 3);
			switch(e) {
				case 0:
				ApplyJiggle(tf, duration);
				break;
				case 1:
				ApplySnap(tf, duration);
				break;
				case 2:
				ApplyThrob(tf, duration);
				break;
				case 3:
				ApplyRotate(tf, duration);
				break;
			}
		}

		public static void ApplyRotate(TransformGroup t, Duration duration)
		{
			var  rotateT = new RotateTransform ();
			t.Children.Add(rotateT);
			var storyboard = CreateDPAnimation(rotateT, "Angle", duration, 0, 360, false, false, new BounceEase { Bounces = 2, Bounciness = 5 });
			storyboard.Begin();
		}

		public static void ApplyJiggle(TransformGroup t, Duration duration)
		{
			var rotateT = new RotateTransform ();
			t.Children.Add(rotateT);
			var storyboard = CreateDPAnimation(rotateT, "Angle", duration, 0, 20, false, false, new ElasticEase { Oscillations = 5 });
			storyboard.Begin();
		}

		public static void ApplySnap(TransformGroup t, Duration duration)
		{
			var scaleT = new ScaleTransform { ScaleY = 1, ScaleX = 1 };
			t.Children.Add(scaleT);
			var storyboard = CreateDPAnimation(scaleT, "ScaleY", duration, 0, 1, false, false, new ElasticEase {  Springiness = 0.4 });
			storyboard.Begin();
		}

		public static void ApplyThrob(TransformGroup t, Duration duration)
		{
			var scaleT = new ScaleTransform { ScaleY = 1, ScaleX = 1 };
			t.Children.Add(scaleT);
			var storyboard = CreateDPAnimation(scaleT, "ScaleY", duration, 0.95, 1, false, false, new ElasticEase { Springiness = 0.4 });
			CreateDPAnimation(scaleT, "ScaleX", duration, 0.95, 1, false, false, new ElasticEase { Springiness = 0.4 }, storyboard);
			storyboard.Begin();
		}
	}
}