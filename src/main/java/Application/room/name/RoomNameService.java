package Application.room.name;

import java.util.List;

/**
 * Created by nikolay on 5/5/17.
 */
public interface RoomNameService {
    public List<RoomName> findAll();
    public RoomName findOne(long id);
    public RoomName findRandom();
    public void saveRoomName(RoomName roomName);
}

