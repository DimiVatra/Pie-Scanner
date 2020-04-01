using System;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Pie_Scanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    [assembly: UsesFeature("android.hardware.camera", Required = false)]
    [assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        ImageView SpyThePieView;
        Bitmap bitmap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

           // var NavCamera = FindViewById<ToolbarItem>(Resource.Id.nav_camera);
            //NavCamera.Click += NavCamera_Click;
        }

        private void NavCamera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if ((requestCode == PickImageId ) && (resultCode == Result.Ok) && (data != null))
            {
                SpyThePieView = FindViewById<ImageView>(Resource.Id.spy_the_pie_pic_view);
                if(data!=null)
                {
                    Android.Net.Uri uri = data.Data;
                    SpyThePieView.SetImageURI(uri);
                    BitmapDrawable bitmapDrawable = ((BitmapDrawable)SpyThePieView.Drawable);
                    if (bitmapDrawable == null)
                    {
                        SpyThePieView.BuildDrawingCache();
                        bitmap = SpyThePieView.GetDrawingCache(false);
                        SpyThePieView.BuildDrawingCache(false);
                    }
                    else
                    {
                        bitmap = bitmapDrawable.Bitmap;
                    }
                }
            }
            else
            {
                SpyThePieView = FindViewById<ImageView>(Resource.Id.spy_the_pie_pic_view);
                if(data!=null)
                {
                    bitmap = (Bitmap)data.Extras.Get("data");
                    SpyThePieView.SetImageBitmap(bitmap);
                }
            }
        }
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            /*View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();*/
            SpyThePyeNavigated();
        }
        int PickImageId = 1000;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                SpyThePyeNavigated();

            }
            else if (id == Resource.Id.nav_gallery)
            {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);

            }
            else if (id == Resource.Id.nav_my_pies)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        void SpyThePyeNavigated()
        {
            
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }
}

