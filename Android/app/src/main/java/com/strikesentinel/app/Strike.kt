package com.strikesentinel.app

import org.json.JSONObject
import android.os.Parcel
import android.os.Parcelable
import android.R.attr.author





class Strike(`in`: Parcel) : Parcelable {
    var id: String? = `in`.readString()
    var tipo: String? = `in`.readString()
    var dataInicio: String? = `in`.readString()
    var dataFim: String? = `in`.readString()
    var observacoes: String? = `in`.readString()
    var todoDia: String? = `in`.readString()
    var estado: String? = `in`.readString()
    var estadoDescr: String? = `in`.readString()
    var cor: String? = `in`.readString()
    var empresa: String? = `in`.readString()
    var sourceLink: String? = `in`.readString()
    var dateGroup: String? = `in`.readString()


    override fun writeToParcel(parcel: Parcel, flags: Int) {
        parcel.writeString(id)
        parcel.writeString(tipo)
        parcel.writeString(dataInicio)
        parcel.writeString(dataFim)
        parcel.writeString(observacoes)
        parcel.writeString(todoDia)
        parcel.writeString(estado)
        parcel.writeString(estadoDescr)
        parcel.writeString(cor)
        parcel.writeString(empresa)
        parcel.writeString(sourceLink)
        parcel.writeString(dateGroup)
    }

    override fun describeContents(): Int {
        return 0
    }

    companion object CREATOR : Parcelable.Creator<Strike> {
        override fun createFromParcel(parcel: Parcel): Strike {
            return Strike(parcel)
        }

        override fun newArray(size: Int): Array<Strike?> {
            return arrayOfNulls(size)
        }
    }

}



class StrikeGroup(strikeJSON: JSONObject) {
    var id: String? = null
    var name: String? = null
    var greves: List<Strike>? = null

}