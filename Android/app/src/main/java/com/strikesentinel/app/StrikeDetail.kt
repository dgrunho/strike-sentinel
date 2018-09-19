package com.strikesentinel.app

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.support.v7.widget.Toolbar
import android.view.Menu
import android.view.View
import android.widget.TextView
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.activity_strike_detail.*


class StrikeDetail : AppCompatActivity() {
    private var strike: Strike? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_strike_detail)

        setToolbar()

        getExtras(savedInstanceState)

        setDatabindings()
    }

    fun getExtras(savedInstanceState: Bundle?){
        if (savedInstanceState == null) {
            val extras = intent.extras
            if (extras == null) {
                strike = null
            } else {
                strike = extras.getParcelable("strike")!!
            }
        } else {
            strike = savedInstanceState.getSerializable("strike") as Strike
        }

    }
    fun setDatabindings(){
        Picasso.get().load(RestService.URL + "StrikeNews/Icon/" + strike!!.id).error(R.drawable.ic_error_orange_24dp).into(ivLogo);
        tvEmpresa.setText(strike!!.empresa)
        tvTipo.setText(strike!!.tipo)
        estado.setText(strike!!.estadoDescr)
        if(strike!!.estado == "Check"){
            ivCheck.visibility = View.VISIBLE
            ivCancel.visibility = View.GONE
            ivQuestion.visibility = View.GONE
        }
        if(strike!!.estado == "Cancel"){
            ivCheck.visibility = View.GONE
            ivCancel.visibility = View.VISIBLE
            ivQuestion.visibility = View.GONE
        }
        if(strike!!.estado == "Help"){
            ivCheck.visibility = View.GONE
            ivCancel.visibility = View.GONE
            ivQuestion.visibility = View.VISIBLE
        }

        if(strike!!.todoDia == "true"){
            tvInicio.setText(ParseDateTime(strike!!.dataInicio, "yyyy-MM-dd"))
            tvFim.setText(ParseDateTime(strike!!.dataFim, "yyyy-MM-dd"))
        } else {
            tvInicio.setText(ParseDateTime(strike!!.dataInicio, "yyyy-MM-dd HH:mm:ss"))
            tvFim.setText(ParseDateTime(strike!!.dataFim, "yyyy-MM-dd HH:mm:ss"))
        }
        tvLink.setText(strike!!.sourceLink)
        tvObs.setText(strike!!.observacoes)

    }
    fun setToolbar(){
        setSupportActionBar(my_toolbar)
        getSupportActionBar()!!.setDisplayHomeAsUpEnabled(true)
        getSupportActionBar()!!.setDisplayShowHomeEnabled(true)
        getSupportActionBar()!!.setDisplayShowTitleEnabled(false)
        my_toolbar.setNavigationIcon(R.drawable.ic_arrow_back_white_24dp)
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        menuInflater.inflate(R.menu.menu_strike_detail, menu)
        return true
    }

    override fun onSupportNavigateUp(): Boolean {
        onBackPressed()
        return true
    }
}
