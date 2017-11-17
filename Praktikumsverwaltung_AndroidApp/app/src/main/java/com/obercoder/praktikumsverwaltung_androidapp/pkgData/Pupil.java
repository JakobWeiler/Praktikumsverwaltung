package com.obercoder.praktikumsverwaltung_androidapp.pkgData;

import android.support.annotation.NonNull;
import android.util.Log;

import org.bson.types.ObjectId;

/**
 * Created by Sasa on 01.10.2017.
 */

public class Pupil implements Comparable<Pupil>{
    private ObjectId id;
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

    public Pupil(ObjectId id, String username, String password, String firstName, String lastname, String currentForm, String email) {
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
       return this.username.compareTo(o.username);
    }

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastname() {
        return lastname;
    }

    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public String getCurrentForm() {
        return currentForm;
    }

    public void setCurrentForm(String currentForm) {
        this.currentForm = currentForm;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }
}