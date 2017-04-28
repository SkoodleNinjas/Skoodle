package Application;

import javax.persistence.*;
import java.awt.Image;

/**
 * Created by nikolay on 4/26/17.
 */
@Entity
public class Topic {

    @Id
    @GeneratedValue(strategy= GenerationType.AUTO)
    private Long id;
    private String topic;
    private String category;
    @Lob
    private byte[] image;

    public Topic(String topic, byte[] image) {
        this.topic = topic;
        this.image = image;
    }

    public Long getId() {
        return id;
    }

    public String getTopic() {
        return topic;
    }

    public byte[] getImage() {
        return image;
    }

    public String getCategory() {
        return category;
    }

    public void setTopic(String topic) {
        this.topic = topic;
    }

    public void setImage(byte[] image) {
        this.image = image;
    }

    public void setCategory(String category) {
        this.category = category;
    }
}
