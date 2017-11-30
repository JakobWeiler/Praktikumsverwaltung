package com.obercoder.praktikumsverwaltung_androidapp.pkgGUI;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import com.obercoder.praktikumsverwaltung_androidapp.R;

public class AddEntryActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_entry);
    }

    public void onBtnStartDate(View view) {
        try {
            startActivity(new Intent(AddEntryActivity.this, EntryStartDateActivity.class));
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }

    public void onBtnEndDate(View view) {
        try {
            startActivity(new Intent(AddEntryActivity.this, EntryEndDateActivity.class));
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }

    public void onBtnAddEntry(View view) {
        try {
            startActivity(new Intent(AddEntryActivity.this, EntryStartDateActivity.class));
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }
}
