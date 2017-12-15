/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import java.time.LocalDate;
import javax.xml.bind.annotation.XmlRootElement;
import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
@XmlRootElement
public class Entry {
    private ObjectId id;
    private LocalDate startDate;
    private LocalDate endDate;
    private double salary;
    private String title;
    private String description;
    private boolean allowedTeacher;
    private boolean allowedAV;
    private ObjectId idPupil;
    private ObjectId idCompany;
    private ObjectId idClass;

    public Entry() {
    }

    public Entry(ObjectId id, LocalDate startDate, LocalDate endDate, double salary, String title, String description, boolean allowedTeacher, boolean allowedAV, ObjectId idPupil, ObjectId idCompany, ObjectId idClass) {
        this.id = id;
        this.startDate = startDate;
        this.endDate = endDate;
        this.salary = salary;
        this.title = title;
        this.description = description;
        this.allowedTeacher = allowedTeacher;
        this.allowedAV = allowedAV;
        this.idPupil = idPupil;
        this.idCompany = idCompany;
        this.idClass = idClass;
    }

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public LocalDate getStartDate() {
        return startDate;
    }

    public void setStartDate(LocalDate startDate) {
        this.startDate = startDate;
    }

    public LocalDate getEndDate() {
        return endDate;
    }

    public void setEndDate(LocalDate endDate) {
        this.endDate = endDate;
    }

    public double getSalary() {
        return salary;
    }

    public void setSalary(double salary) {
        this.salary = salary;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public boolean isAllowedTeacher() {
        return allowedTeacher;
    }

    public void setAllowedTeacher(boolean allowedTeacher) {
        this.allowedTeacher = allowedTeacher;
    }

    public boolean isAllowedAV() {
        return allowedAV;
    }

    public void setAllowedAV(boolean allowedAV) {
        this.allowedAV = allowedAV;
    }

    public ObjectId getIdPupil() {
        return idPupil;
    }

    public void setIdPupil(ObjectId idPupil) {
        this.idPupil = idPupil;
    }

    public ObjectId getIdCompany() {
        return idCompany;
    }

    public void setIdCompany(ObjectId idCompany) {
        this.idCompany = idCompany;
    }

    public ObjectId getIdClass() {
        return idClass;
    }

    public void setIdClass(ObjectId idClass) {
        this.idClass = idClass;
    }
    
    
}
