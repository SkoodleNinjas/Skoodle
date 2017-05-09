package Application.room.name;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

/**
 * Created by nikolay on 5/5/17.
 */
@Entity
public class RoomName {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;

    private String roomName;

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getRoomName() {
        return roomName;
    }

    public void setRoomName(String roomName) {
        this.roomName = roomName;
    }
}
