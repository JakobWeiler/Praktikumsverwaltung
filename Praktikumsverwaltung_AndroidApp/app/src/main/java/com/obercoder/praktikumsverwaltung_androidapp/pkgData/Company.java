package com.obercoder.praktikumsverwaltung_androidapp.pkgData;

import org.bson.types.ObjectId;

/**
 * Created by Sasa on 17.11.2017.
 */

public class Company {
    private ObjectId id;
    private String name;
    private String location;
    private int numberOfEmployees;
    private String contactPerson;


    public Company(ObjectId id, String name, String location, int numberOfEmployees, String contactPerson) {
        this.id = id;
        this.name = name;
        this.location = location;
        this.numberOfEmployees = numberOfEmployees;
        this.contactPerson = contactPerson;
    }

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public int getNumberOfEmployees() {
        return numberOfEmployees;
    }

    public void setNumberOfEmployees(int numberOfEmployees) {
        this.numberOfEmployees = numberOfEmployees;
    }

    public String getContactPerson() {
        return contactPerson;
    }

    public void setContactPerson(String contactPerson) {
        this.contactPerson = contactPerson;
    }
}
