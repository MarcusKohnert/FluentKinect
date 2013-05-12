FluentKinect
============

Set up a Kinect Sensor in a fluent style.

var sensor = KinectSensor.KinectSensors
                         .FirstOrDefault(_ => _.Status == KinectStatus.Connected);
if (sensor == null) throw new InvalidOperationException("No kinect connected");

sensor.EnableColorStream()
      .EnableSkeletonStream()
      .EnableDepthStream()
      .Seated()
      .NearMode()
      .Start();
