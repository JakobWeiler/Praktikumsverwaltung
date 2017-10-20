package com.obercoder.praktikumsverwaltung_androidapp.pkgGUI;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import com.obercoder.praktikumsverwaltung_androidapp.R;

public class EntryStartDateActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_entry_start_date);
    }

    public void onBtnSaveStartDate(View view) {
        try {
            finish();
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }
}
