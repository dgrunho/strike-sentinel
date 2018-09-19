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
        public val URL = "http://strikesentinel-dev.ddns.net/api/"
    }
}