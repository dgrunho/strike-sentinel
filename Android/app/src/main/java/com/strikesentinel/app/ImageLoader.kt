package com.strikesentinel.app

import com.android.volley.toolbox.ImageLoader
import android.graphics.Bitmap
import com.android.volley.RequestQueue
import android.util.LruCache;


private fun imageLoader() {
    val mRequestQueue = VolleySingleton.instance?.requestQueue

    val imageLoader = ImageLoader(mRequestQueue, object : ImageLoader.ImageCache {
        private val mCache = LruCache<String, Bitmap>(10)

        override fun putBitmap(url: String, bitmap: Bitmap) {
            mCache.put(url, bitmap)
        }

        override fun getBitmap(url: String): Bitmap {
            return mCache.get(url)
        }
    })

}