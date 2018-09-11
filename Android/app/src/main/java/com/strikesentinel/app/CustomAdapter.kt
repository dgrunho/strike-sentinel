package com.strikesentinel.app

import android.support.v7.app.AppCompatActivity
import android.os.Bundle

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import org.json.JSONArray
import org.json.JSONException
import com.android.volley.VolleyError
import android.graphics.Bitmap
import com.android.volley.Response.Listener
import com.android.volley.toolbox.ImageRequest
import com.android.volley.toolbox.NetworkImageView;
import com.android.volley.toolbox.Volley
import com.android.volley.toolbox.ImageLoader
import kotlinx.android.synthetic.main.view_strike_entry.view.*


class CustomAdapter(context: Context, resource: Int, student: List<Strike>) : ArrayAdapter<Strike>(context, resource, student) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View? {

        var v = convertView

        if (v == null) {
            val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = inflater.inflate(R.layout.view_strike_entry, parent, false)
        }

        val student = getItem(position)

        if (student != null) {
            val tvStudentId = v!!.findViewById(R.id.tipo) as TextView
            val tvStudentName = v.findViewById(R.id.dataInicio) as TextView
            tvStudentId.text = student.tipo
            tvStudentName.setText(student.empresa)
        }

        refreshScreenVolley(position, convertView, parent)
        return v
    }

    private fun refreshScreenVolley(position: Int, convertView: View?, parent: ViewGroup) {




        var v = convertView

        if (v == null) {
            val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = inflater.inflate(R.layout.view_strike_entry, parent, false)
        }

        val Id = v!!.findViewById(R.id.tipo) as TextView

        val strike = getItem(position)




        val imageUrl = "https://raw.githubusercontent.com/AndroidCodility/Picasso-RecyclerView/master/images/marshmallow.png"
        VolleySingleton.instance?.addToRequestQueue(ImageRequest(imageUrl, Response.Listener<Bitmap> { response ->
            v.networkImageView2.setImageBitmap(response)
           // val Img = v!!.findViewById(R.id.networkImageView) as NetworkImageView
            //Img.setImageBitmap(response)
        }, 0, 0, null, Response.ErrorListener { error ->
            Toast.makeText(context, error.message, Toast.LENGTH_SHORT).show()
        }))

        /*val request = ImageRequest("http://loremflickr.com/800/600/cat?random=1",
                Listener { bitmap -> try {
                    Toast.makeText(context, "aaaaa", Toast.LENGTH_LONG).show()
                    val Img = v!!.findViewById(R.id.imIcon) as ImageView
                    Img.setImageBitmap(bitmap)
                } catch (e: JSONException) {
                    e.printStackTrace()
                } }, 0, 0, null,
                Response.ErrorListener { volleyError -> Toast.makeText(context, volleyError.message, Toast.LENGTH_LONG).show() })
// Access the RequestQueue through your singleton class.
        VolleySingleton.instance?.addToRequestQueue(request)*/



    }
}