package com.strikesentinel.app

import java.text.ParseException
import java.text.SimpleDateFormat
import java.util.*

fun ParseDateTime(dateString: String?, formatOut: String): String? {
    var returnValue: String =""

    var dataOut : Date? = null
    val format = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss")
    try {
        dataOut = format.parse(dateString)
        val formatterOut = SimpleDateFormat(formatOut)
        returnValue = formatterOut.format(dataOut)
    } catch (e: ParseException) {
        e.printStackTrace()
    }

    return returnValue
}
