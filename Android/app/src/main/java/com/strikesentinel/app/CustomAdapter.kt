package com.strikesentinel.app

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ImageView
import android.widget.ListView
import android.widget.TextView
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.activity_main.*
import java.text.ParseException
import java.text.SimpleDateFormat
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.time.format.FormatStyle
import java.util.*


class StrikeEntryAdapter(context: Context, resource: Int, strikes: List<Strike>) : ArrayAdapter<Strike>(context, resource, strikes) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View? {

        var v = convertView

        if (v == null) {
            val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = inflater.inflate(R.layout.strike_entry, parent, false)
        }

        val strike = getItem(position)

        if (strike != null) {
            val empresa = v!!.findViewById(R.id.empresa) as TextView
            empresa.setText(strike.empresa)

            val datainicio = v!!.findViewById(R.id.datainicio) as TextView
            val datafim = v!!.findViewById(R.id.datafim) as TextView
            if(strike.todoDia == "true"){
                datainicio.setText(ParseDateTime(strike.dataInicio, "yyyy-MM-dd"))
                datafim.setText(ParseDateTime(strike.dataFim, "yyyy-MM-dd"))
            } else {
                datainicio.setText(ParseDateTime(strike.dataInicio, "yyyy-MM-dd HH:mm:ss"))
                datafim.setText(ParseDateTime(strike.dataFim, "yyyy-MM-dd HH:mm:ss"))
            }

            val ivCheck = v!!.findViewById(R.id.ivCheck) as ImageView
            val ivCancel = v!!.findViewById(R.id.ivCancel) as ImageView
            val ivQuestion = v!!.findViewById(R.id.ivQuestion) as ImageView
            if(strike.estado == "Check"){
                ivCheck.visibility = View.VISIBLE
                ivCancel.visibility = View.GONE
                ivQuestion.visibility = View.GONE
            }
            if(strike.estado == "Cancel"){
                ivCheck.visibility = View.GONE
                ivCancel.visibility = View.VISIBLE
                ivQuestion.visibility = View.GONE
            }
            if(strike.estado == "Help"){
                ivCheck.visibility = View.GONE
                ivCancel.visibility = View.GONE
                ivQuestion.visibility = View.VISIBLE
            }
            val iv = v!!.findViewById(R.id.imageView) as ImageView
            Picasso.get().load(RestService.URL + "StrikeNews/Icon/" + strike.id).error(R.drawable.ic_error_orange_24dp).into(iv);
        }

        return v
    }
}

class StrikeGroupAdapter(context: Context, resource: Int, strikes: List<StrikeGroup>) : ArrayAdapter<StrikeGroup>(context, resource, strikes) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View? {

        var v = convertView

        if (v == null) {
            val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = inflater.inflate(R.layout.strike_entry_header, parent, false)
        }

        val strike_group = getItem(position)

        if (strike_group != null) {

            val groupName = v!!.findViewById(R.id.group_name) as TextView
            groupName.setText(strike_group.name)

            val lv = v!!.findViewById(R.id.lvStrikes) as ListView
            lv.adapter = StrikeEntryAdapter(context, R.layout.strike_entry, strike_group.greves!!.toList())
        }

        return v
    }
}