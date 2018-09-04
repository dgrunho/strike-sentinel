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





class MainActivity : AppCompatActivity() {

    var listView: ListView? = null
    var restService: RestService = RestService()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    //This function will call when the screen is activate
    fun onClick(v: View) {
        refreshScreen()
    }

    private fun refreshScreen() {
        //Call to server to grab list of student records. this is a asyn
        restService.service.getStrike(object : Callback<List<Strike>> {
            override fun success(students: List<Strike>, response: Response) {
                val lv = findViewById<View>(R.id.listView) as ListView

                val customAdapter = CustomAdapter(this@MainActivity, R.layout.view_strike_entry, students)

                /*lv.onItemClickListener = AdapterView.OnItemClickListener { parent, view, position, id ->
                    student_Id = view.findViewById(R.id.student_Id) as TextView
                    val studentId = student_Id.getText().toString()
                    val objIndent = Intent(applicationContext, StudentDetail::class.java)
                    objIndent.putExtra("student_Id", Integer.parseInt(studentId))
                    startActivity(objIndent)
                }*/
                lv.adapter = customAdapter
            }

            override fun failure(error: RetrofitError) {
                Toast.makeText(this@MainActivity, error.message.toString(), Toast.LENGTH_LONG).show()
            }
        })


    }
}
