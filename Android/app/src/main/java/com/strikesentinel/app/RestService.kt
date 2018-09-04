package com.strikesentinel.app

class RestService {
    private val restAdapter: retrofit.RestAdapter
    val service: InstituteService

    init {
        restAdapter = retrofit.RestAdapter.Builder()
                .setEndpoint(URL)
                .setLogLevel(retrofit.RestAdapter.LogLevel.FULL)
                .build()

        service = restAdapter.create(InstituteService::class.java)
    }

    companion object {
        //You need to change the IP if you testing environment is not local machine
        //or you may have different URL than we have here
        private val URL = "http://strikesentinel.ddns.net/api/"
    }
}