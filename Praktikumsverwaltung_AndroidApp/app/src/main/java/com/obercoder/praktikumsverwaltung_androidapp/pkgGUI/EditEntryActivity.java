package com.obercoder.praktikumsverwaltung_androidapp.pkgGUI;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ImageView;

import com.mongodb.MongoClient;
import com.mongodb.MongoClientURI;
import com.obercoder.praktikumsverwaltung_androidapp.R;

public class EditEntryActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit_entry);
    }

    /*
    MongoClientURI mongoUri  = new MongoClientURI("");
    MongoClient mongoClient = new MongoClient(mongoUri);
    MongoDatabase db = mongoClient.getDatabase(uri.getDatabase());
    */
}
