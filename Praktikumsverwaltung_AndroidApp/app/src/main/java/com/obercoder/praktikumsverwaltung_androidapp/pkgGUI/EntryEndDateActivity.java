package com.obercoder.praktikumsverwaltung_androidapp.pkgGUI;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import com.obercoder.praktikumsverwaltung_androidapp.R;

public class EntryEndDateActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_entry_end_date);
    }

    public void onBtnSaveEndDate(View view) {
        try {
            finish();
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }
}
