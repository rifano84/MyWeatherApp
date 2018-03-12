package md5daa2a94e7c0461cb705421c2d03e73e0;


public class FileAccess
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.FormsAppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MyWeatherApp.Droid.FileAccess, MyWeatherApp.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FileAccess.class, __md_methods);
	}


	public FileAccess ()
	{
		super ();
		if (getClass () == FileAccess.class)
			mono.android.TypeManager.Activate ("MyWeatherApp.Droid.FileAccess, MyWeatherApp.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
