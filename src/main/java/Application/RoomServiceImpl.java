package Application;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;
import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */

@Service
@Transactional
public class RoomServiceImpl implements RoomService{
    @Autowired
    private RoomRepository roomRepository;

    @Override
    public List<Room> findAll() {
        return roomRepository.findAll();
    }

    @Override
    public Room findOne(long id) {
        return roomRepository.findOne(id);
    }

    @Override
    public void saveRoom(Room room) {
        roomRepository.save(room);
    }
}
