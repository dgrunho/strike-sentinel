package com.strikesentinel.app

import android.content.Context
import android.content.Intent
import android.support.v4.content.ContextCompat.startActivity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import com.squareup.picasso.Picasso
import android.support.v4.content.ContextCompat.startActivity




class StrikeEntryAdapter(context: Context, resource: Int, strikes: List<Strike>) : ArrayAdapter<Strike>(context, resource, strikes) {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View? {

        var v = convertView

        if (v == null) {
            val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
            v = inflater.inflate(R.layout.strike_entry, parent, false)!!
        }

        val strike = getItem(position)

        if (strike != null) {
            val idStrike = v.findViewById(R.id.idStrike) as TextView
            idStrike.setText(strike.id)

            val empresa = v.findViewById(R.id.empresa) as TextView
            empresa.setText(strike.empresa)

            val datainicio = v.findViewById(R.id.datainicio) as TextView
            val datafim = v.findViewById(R.id.datafim) as TextView
            if(strike.todoDia == "true"){
                datainicio.setText(ParseDateTime(strike.dataInicio, "yyyy-MM-dd"))
                datafim.setText(ParseDateTime(strike.dataFim, "yyyy-MM-dd"))
            } else {
                datainicio.setText(ParseDateTime(strike.dataInicio, "yyyy-MM-dd HH:mm:ss"))
                datafim.setText(ParseDateTime(strike.dataFim, "yyyy-MM-dd HH:mm:ss"))
            }

            val ivCheck = v.findViewById(R.id.ivCheck) as ImageView
            val ivCancel = v.findViewById(R.id.ivCancel) as ImageView
            val ivQuestion = v.findViewById(R.id.ivQuestion) as ImageView
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
            val iv = v.findViewById(R.id.imageView) as ImageView
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

            lv.onItemClickListener = AdapterView.OnItemClickListener { parent, view, position2, id ->
                    val idStrike = view.findViewById(R.id.idStrike) as TextView
                    val id = idStrike.getText().toString()
                    var strike: Strike? = null
                    val sg = (parent.adapter) as StrikeEntryAdapter
                    for (i in 0..sg.count-1) {
                        val str: Strike? = sg.getItem(i)
                        if (str!!.id == id){
                            strike = str;
                        }
                    }

                    val objIntent = Intent(context, StrikeDetail::class.java)
                    objIntent.putExtra("strike_Id", Integer.parseInt(id))
                    objIntent.putExtra("strike", strike!!)
                    startActivity(context, objIntent, null)


                }

            lv.adapter = StrikeEntryAdapter(context, R.layout.strike_entry, strike_group.greves!!.toList())
        }

        return v
    }
}