package com.obercoder.praktikumsverwaltung_androidapp.pkgData;

import android.support.annotation.NonNull;
import android.util.Log;

/**
 * Created by Sasa on 01.10.2017.
 */

public class Pupil implements Comparable<Pupil>{
    private int id;
    private String username;
    private String password;
    private String firstName;
    private String lastname;
    private String currentForm;
    private String email;

    public Pupil (String username, String password) {
        this.username=username;
        this.password=password;
    }

    public Pupil(int id, String username, String password, String firstName, String lastname, String currentForm, String email) {
        this.id = id;
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.lastname = lastname;
        this.currentForm = currentForm;
        this.email = email;
    }

    @Override
    public boolean equals(Object o) {
        Log.d("equals","dasdsa");
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        Pupil pupil = (Pupil) o;
        if (!username.equals(pupil.username) && (!email.equals(pupil.email))) return false;
        return password.equals(pupil.password);
    }

    @Override
    public int hashCode() {
        int result = username.hashCode();
        result = 31 * result + password.hashCode();
        return result;
    }

    @Override
    public int compareTo(@NonNull Pupil o) {
        Log.d("comp","dasdsa"); return this.username.compareTo(o.username);
    }
}