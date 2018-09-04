package com.strikesentinel.app

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView


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

        return v
    }
}