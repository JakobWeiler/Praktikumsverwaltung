package com.obercoder.praktikumsverwaltung_androidapp.pkgData;

import java.util.TreeSet;

/**
 * Created by Sasa on 01.10.2017.
 */

public class Database {

    private TreeSet<Pupil> tsPupil = new TreeSet<Pupil>();
    private static Database singletonDB = null;

    public static Database newInstance() {
        if (singletonDB == null) {
            singletonDB = new Database();
        }
        return  singletonDB;
    }

    public void addPupil (Pupil p) {
        tsPupil.add(p);
    }

    public TreeSet<Pupil> getTsPupil (){
        return tsPupil;
    }
}
