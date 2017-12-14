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
    private ObjectId id;
    private String description;
    private ObjectId idKV;

    public Class() {
    }

    public Class(ObjectId id, String description, ObjectId idKV) {
        this.id = id;
        this.description = description;
        this.idKV = idKV;
    }

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public ObjectId getIdKV() {
        return idKV;
    }

    public void setIdKV(ObjectId idKV) {
        this.idKV = idKV;
    }
    
    
}
