package md5daa2a94e7c0461cb705421c2d03e73e0;


public class LocationRequest
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.FormsAppCompatActivity
	implements
		mono.android.IGCUserPeer,
		android.support.v4.app.ActivityCompat.OnRequestPermissionsResultCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"";
		mono.android.Runtime.register ("MyWeatherApp.Droid.LocationRequest, MyWeatherApp.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocationRequest.class, __md_methods);
	}


	public LocationRequest ()
	{
		super ();
		if (getClass () == LocationRequest.class)
			mono.android.TypeManager.Activate ("MyWeatherApp.Droid.LocationRequest, MyWeatherApp.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);

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
