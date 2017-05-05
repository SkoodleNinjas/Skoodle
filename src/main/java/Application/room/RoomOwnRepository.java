package Application.room;

import Application.room.topic.Topic;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */
public interface RoomOwnRepository extends Repository {
    List<Room> findByTopic(Topic topic);
    List<Room> findByMaxPlayers(long maxPlayers);
}
