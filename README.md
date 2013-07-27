FluentKinect
============

Set up a Kinect Sensor in a fluent style.

var sensor = await KinectConnector.GetKinect();<br />

sensor.EnableColorStream() <br />
      .EnableSkeletonStream()<br />
      .EnableDepthStream()<br />
      .Seated()<br />
      .NearMode() <br />
      .Start();<br />
