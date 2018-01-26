/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgBeans;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import pkgData.Database;

/**
 *
 * @author Jakob
 */
@ManagedBean
public class EntryBean {
    @ManagedProperty(value="#{database}")
    private Database database;
    
    public EntryBean() {
        try {
            database.loadEntries();
        } catch (Exception ex) {
            System.out.println("error in EntryBean: " + ex.getMessage());
        }
    }
    
    public void setDatabase(Database database){
        this.database = database;
    }
}
