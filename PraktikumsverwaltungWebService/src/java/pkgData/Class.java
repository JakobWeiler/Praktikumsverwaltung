/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import javax.xml.bind.annotation.XmlRootElement;
import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
@XmlRootElement
public class Class {
    private String id;
    private String description;
    private String idKV;

    public Class() {
    }

    public Class(String id, String description, String idKV) {
        this.id = id;
        this.description = description;
        this.idKV = idKV;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getIdKV() {
        return idKV;
    }

    public void setIdKV(String idKV) {
        this.idKV = idKV;
    }
    
    
}
