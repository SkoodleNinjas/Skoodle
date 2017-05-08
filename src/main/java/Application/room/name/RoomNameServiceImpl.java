package Application.room.name;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Random;

/**
 * Created by nikolay on 5/5/17.
 */
@Service
@Transactional
public class RoomNameServiceImpl implements RoomNameService {
    @Autowired
    private RoomNameRepository roomNameRepository;

    @Override
    public List<RoomName> findAll() {
        return roomNameRepository.findAll();
    }

    @Override
    public RoomName findOne(long id) {
        return roomNameRepository.findOne(id);
    }

    @Override
    public RoomName findRandom() {
        Random random = new Random();
        return roomNameRepository.findOne((long) (random.nextInt((int) ((roomNameRepository.count() - 1) - 0 + 1)) + 0));
    }

    @Override
    public void saveRoomName(RoomName roomName) {
        roomNameRepository.save(roomName);
    }
}
