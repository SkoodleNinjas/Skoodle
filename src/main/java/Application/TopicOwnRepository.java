package Application;

import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Created by nikolay on 4/27/17.
 */
public interface TopicOwnRepository extends Repository {
    List<Topic> findByCategory(String category);
}
