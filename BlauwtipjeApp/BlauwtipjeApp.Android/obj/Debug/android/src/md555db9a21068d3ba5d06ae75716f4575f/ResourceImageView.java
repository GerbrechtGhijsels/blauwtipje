package md555db9a21068d3ba5d06ae75716f4575f;


public class ResourceImageView
	extends ffimageloading.views.ImageViewAsync
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setImageBitmap:(Landroid/graphics/Bitmap;)V:GetSetImageBitmap_Landroid_graphics_Bitmap_Handler\n" +
			"";
		mono.android.Runtime.register ("BlauwtipjeApp.Droid.Components.Image.ResourceImageView, BlauwtipjeApp.Android", ResourceImageView.class, __md_methods);
	}


	public ResourceImageView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ResourceImageView.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.Image.ResourceImageView, BlauwtipjeApp.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ResourceImageView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ResourceImageView.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.Image.ResourceImageView, BlauwtipjeApp.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public ResourceImageView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ResourceImageView.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.Image.ResourceImageView, BlauwtipjeApp.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ResourceImageView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ResourceImageView.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.Image.ResourceImageView, BlauwtipjeApp.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void setImageBitmap (android.graphics.Bitmap p0)
	{
		n_setImageBitmap (p0);
	}

	private native void n_setImageBitmap (android.graphics.Bitmap p0);

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
