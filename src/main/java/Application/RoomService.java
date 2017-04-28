package Application;

import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */
public interface RoomService {
    public List<Room> findAll();
    public Room findOne(long id);
    public void saveRoom(Room room);
}

