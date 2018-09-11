package com.strikesentinel.app

import android.support.v7.app.AppCompatActivity
import android.os.Bundle

import android.view.View;
import android.widget.ListView;
import android.widget.Toast;
import android.widget.ProgressBar;
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import org.json.JSONException
import org.json.JSONObject
import org.json.JSONArray
import com.android.volley.toolbox.NetworkImageView;
import com.android.volley.Response
import com.android.volley.toolbox.ImageRequest
import com.android.volley.toolbox.Volley
import android.graphics.Bitmap
import kotlinx.android.synthetic.main.activity_main.*


class MainActivity : AppCompatActivity() {

    var listView: ListView? = null
    private val imageUrl = "https://raw.githubusercontent.com/AndroidCodility/Picasso-RecyclerView/master/images/marshmallow.png"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        supportActionBar!!.setDisplayHomeAsUpEnabled(true);
    }

    override fun onResume() {
        super.onResume()
        refreshScreenVolley()
    }

    private fun refreshScreenVolley() {
        val pb = findViewById<View>(R.id.pbLoad) as ProgressBar
        pb.isIndeterminate = true
        pb.setVisibility(ProgressBar.VISIBLE);
        val stringRequest = StringRequest(Request.Method.GET,
                EndPoints.URL_GET_STRIKES,
                com.android.volley.Response.Listener<String> { s ->
                    try {
                        val array = JSONArray(s)
                        var ListStrike: MutableList<Strike> = mutableListOf<Strike>()
                        for (i in 0..array.length() - 1 - 1) {
                            ListStrike.add(Strike(array.getJSONObject(i)))
                        }

                        val lv = findViewById<View>(R.id.listView) as ListView
                        val customAdapter = CustomAdapter(this@MainActivity, R.layout.view_strike_entry, ListStrike)
                        lv.adapter = customAdapter

                        pb.setVisibility(ProgressBar.INVISIBLE);
                    } catch (e: JSONException) {
                        e.printStackTrace()
                    }
                }, com.android.volley.Response.ErrorListener { volleyError -> Toast.makeText(applicationContext, volleyError.message, Toast.LENGTH_LONG).show()})
        VolleySingleton.instance?.addToRequestQueue(stringRequest)
    }
}
