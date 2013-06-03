FluentKinect
============

Set up a Kinect Sensor in a fluent style.

var sensor = KinectSensor.KinectSensors.FirstOrDefault(_ => _.Status == KinectStatus.Connected); <br />
if (sensor == null) throw new InvalidOperationException("No kinect connected"); <br />

sensor.EnableColorStream() <br />
      .EnableSkeletonStream()<br />
      .EnableDepthStream()<br />
      .Seated()<br />
      .NearMode() <br />
      .Start();<br />
