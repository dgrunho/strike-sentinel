package com.strikesentinel.app

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ListView
import android.widget.TextView
import kotlinx.android.synthetic.main.activity_main.*


class CustomAdapter(context: Context, resource: Int, strikes: List<Strike>) : ArrayAdapter<Strike>(context, resource, strikes) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View? {

        var v = convertView

        if (v == null) {
            val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = inflater.inflate(R.layout.strike_entry, parent, false)
        }

        val strike = getItem(position)

        if (strike != null) {
            val tvStudentName = v!!.findViewById(R.id.dataInicio) as TextView
            tvStudentName.setText(strike.empresa)
        }

        return v
    }
}

class CustomAdapterGroups(context: Context, resource: Int, strikes: List<StrikeGroup>) : ArrayAdapter<StrikeGroup>(context, resource, strikes) {

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
            lv.adapter = CustomAdapter(context, R.layout.strike_entry, strike_group.greves!!.toList())
        }

        return v
    }
}