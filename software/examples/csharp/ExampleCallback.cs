using Tinkerforge;

class Example
{
	private static string HOST = "localhost";
	private static int PORT = 4223;
	private static string UID = "bAc"; // Change to your UID

	// Callback function for air pressure callback (parameter has unit mbar/1000)
	static void AirPressureCB(object sender, int airPressure)
	{
		System.Console.WriteLine("Air Pressure: " + airPressure/1000.0 + " mbar");
	}

	// Callback function for altitude callback (parameter has unit cm)
	static void AltitudeCB(object sender, int altitude)
	{
		System.Console.WriteLine("Altitude: " + altitude/100.0 + " m");
	}

	static void Main()
	{
		IPConnection ipcon = new IPConnection(); // Create IP connection
		BrickletBarometer b = new BrickletBarometer(UID, ipcon); // Create device object

		ipcon.Connect(HOST, PORT); // Connect to brickd
		// Don't use device before ipcon is connected

		// Set Period for air pressure and altitude callbacks to 1s (1000ms)
		// Note: The air pressure and altitude callbacks are only called every second
		//       if the air pressure or altitude has changed since the last call!
		b.SetAirPressureCallbackPeriod(1000);
		b.SetAltitudeCallbackPeriod(1000);

		// Register air pressure callback to function AirPressureCB
		b.AirPressure += AirPressureCB;

		// Register altitude callback to function AltitudeCB
		b.Altitude += AltitudeCB;

		System.Console.WriteLine("Press key to exit");
		System.Console.ReadKey();
	}
}
