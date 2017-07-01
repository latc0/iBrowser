namespace iphone
{
	struct Device
	{
		private string _name;
		private string _version;
		private System.IntPtr _dev;
		public string Name
		{
			get { return _name; }
			set { if (value != null)_name = value; }
		}
		public string Version
		{
			get { return _version; }
			set { if (value != null)_version = value; }
		}
		public System.IntPtr Handle
		{
			get { return _dev; }
			set { if (value != null)_dev = value; }
		}
	}
}