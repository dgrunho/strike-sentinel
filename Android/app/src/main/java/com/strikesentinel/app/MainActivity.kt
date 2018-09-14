package com.strikesentinel.app

import android.support.v7.app.AppCompatActivity
import android.os.Bundle

import android.view.View;
import android.widget.Toast;
import kotlinx.android.synthetic.main.activity_main.*
import android.widget.AdapterView

import retrofit.Callback;
import retrofit.RetrofitError;
import retrofit.client.Response;
import android.view.Menu


class MainActivity : AppCompatActivity() {
    var restService: RestService = RestService()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        setSupportActionBar(my_toolbar)

        //Se quiser por o icon
        //getSupportActionBar()!!.setLogo(R.mipmap.ic_launcher);
        //getSupportActionBar()!!.setDisplayUseLogoEnabled(true);

        my_toolbar.setNavigationIcon(R.drawable.ic_menu_24dp)
        my_toolbar.setNavigationOnClickListener(
                View.OnClickListener {
                    Toast.makeText(this@MainActivity, "Toolbar", Toast.LENGTH_SHORT).show()
                }
        )

        pbLoad.setVisibility(View.GONE)

        lvStrikes.onItemClickListener = AdapterView.OnItemClickListener { parent, view, position, id ->
            Toast.makeText(this@MainActivity, "Click", Toast.LENGTH_LONG).show()
        }
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        menuInflater.inflate(R.menu.menu_main, menu)
        return true
    }

    override fun onResume() {
        super.onResume()
        refreshScreen()
    }

    private fun refreshScreen() {
        pbLoad.setVisibility(View.VISIBLE)
        //Call to server to grab list of student records. this is a asyn
        restService.service.getStrikeGroup(object : Callback<List<StrikeGroup>> {
            override fun success(strikegroups: List<StrikeGroup>, response: Response) {
                //val adapter = CustomAdapterGroups(this@MainActivity, R.layout.strike_entry, strikegroups)
                lvStrikes.adapter = CustomAdapterGroups(this@MainActivity, R.layout.strike_entry_header, strikegroups)
                pbLoad.setVisibility(View.GONE)
            }

            override fun failure(error: RetrofitError) {
                Toast.makeText(this@MainActivity, error.message.toString(), Toast.LENGTH_LONG).show()
                pbLoad.setVisibility(View.GONE)
            }
        })
        /*restService.service.getStrike(object : Callback<List<Strike>> {
            override fun success(students: List<Strike>, response: Response) {
                val adapter = CustomAdapter(this@MainActivity, R.layout.strike_entry, students)
                lvStrikes.adapter = CustomAdapter(this@MainActivity, R.layout.strike_entry, students)
                pbLoad.setVisibility(View.GONE)
            }

            override fun failure(error: RetrofitError) {
                Toast.makeText(this@MainActivity, error.message.toString(), Toast.LENGTH_LONG).show()
                pbLoad.setVisibility(View.GONE)
            }
        })*/


    }
}
