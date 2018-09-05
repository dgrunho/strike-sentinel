package com.strikesentinel.app

import android.support.v7.app.AppCompatActivity
import android.os.Bundle

import retrofit.Callback;
import retrofit.RetrofitError;
import retrofit.client.Response;
import android.widget.TextView
import android.view.View;
import android.widget.ListView;
import android.widget.Toast;
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import org.json.JSONException
import org.json.JSONObject
import org.json.JSONArray





class MainActivity : AppCompatActivity() {

    var listView: ListView? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    //This function will call when the screen is activate
    fun onClick(v: View) {
        refreshScreenVolley()
    }
    private fun refreshScreenVolley() {
        val stringRequest = StringRequest(Request.Method.GET,
                EndPoints.URL_GET_ARTIST,
                com.android.volley.Response.Listener<String> { s ->
                    try {
                        val array = JSONArray(s)
                        Toast.makeText(getApplicationContext(), array.toString(), Toast.LENGTH_LONG).show()

                        //val array = obj.getJSONArray("artists")
                        var ListStrike: MutableList<Strike> = mutableListOf<Strike>()
                        for (i in 0..array.length() - 1) {
                            val objectArtist = array.getJSONObject(i)
                            val artist = Strike()
                            artist.tipo = objectArtist.getString("tipo")
                            artist.empresa = objectArtist.getString("empresa")

                            ListStrike.add(artist)
                            val lv = findViewById<View>(R.id.listView) as ListView

                            val customAdapter = CustomAdapter(this@MainActivity, R.layout.view_strike_entry, ListStrike)

                            lv.adapter = customAdapter
                        }

                        /*if (!obj.getBoolean("error")) {
                            val array = obj.getJSONArray("artists")
                            var ListStrike: MutableList<Strike> = mutableListOf<Strike>()
                            for (i in 0..array.length() - 1) {
                                val objectArtist = array.getJSONObject(i)
                                val artist = Strike()
                                artist.tipo = objectArtist.getString("tipo")
                                artist.empresa = objectArtist.getString("empresa")

                                ListStrike.add(artist)
                                val lv = findViewById<View>(R.id.listView) as ListView

                                val customAdapter = CustomAdapter(this@MainActivity, R.layout.view_strike_entry, ListStrike)

                                lv.adapter = customAdapter
                            }
                        } else {
                            Toast.makeText(getApplicationContext(), obj.getString("message"), Toast.LENGTH_LONG).show()
                        }*/
                    } catch (e: JSONException) {
                        e.printStackTrace()
                    }
                }, com.android.volley.Response.ErrorListener { volleyError -> Toast.makeText(applicationContext, volleyError.message, Toast.LENGTH_LONG).show()})
        VolleySingleton.instance?.addToRequestQueue(stringRequest)
    }
}
