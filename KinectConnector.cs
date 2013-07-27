namespace FluentKinect
{
	using System;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Kinect;

	public sealed class KinectConnector
	{
		private static KinectSensor kinectSensor;
		private static CoordinateMapper coordinateMapper;
		
		/// <summary>
		/// Starts a new Task and listens to KinectSensors StatusChanged event.
		/// </summary>
		/// <returns>Eventually returns a kinect sensor when one is connected.</returns>
		public static Task<KinectSensor> GetKinect()
		{
			if (kinectSensor != null)
			{
				var task = new Task<KinectSensor>(() => { return kinectSensor; });
				task.Start();
				return task;
			}

			var kinect = KinectSensor.KinectSensors.FirstOrDefault(_ => _.Status == KinectStatus.Connected);
			if (kinect != null)
			{
				kinectSensor = kinect;
				var task = new Task<KinectSensor>(() => { return kinectSensor; });
				task.Start();
				return task;
			}

			var t = new Task<KinectSensor>(() =>
			{
				KinectSensor sensor = null;
				using (var signal = new ManualResetEventSlim())
				{
					KinectSensor.KinectSensors.StatusChanged += (s, e) =>
					{
						if (e.Status == KinectStatus.Connected)
						{
							kinectSensor = e.Sensor;
							coordinateMapper = new CoordinateMapper(kinectSensor);
							signal.Set();
						}
					};

					signal.Wait();
				}

				return kinectSensor;
			});
			t.Start();
			return t;
		}

		public static CoordinateMapper GetCoordinateMapper()
		{
			return coordinateMapper;
		}
	}
}