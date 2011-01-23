#include "IGraphElementBase.h"

#ifndef _IGraphElement_h_
#define _IGraphElement_h_

using namespace std ;

class IGraphElement : public IGraphElementBase{

	// We should define prototype of functions that will be subscribers to our data
	typedef void FuncCharPtr(char*, int) ;

public:
	struct GetResultStructure
	{
		int length;
		char* ptr;
	};

	// initGet sets up a pointer holding a copy of pointer of data we want to return on Get() call
	void InitGet(char * pointerToUseInGetOperations, int pointerToUseInGetOperationsSize)
	{
		pointerToGet = pointerToUseInGetOperations;
		pointerToGetSize = pointerToUseInGetOperationsSize;
	}

	// Function for adding subscribers functions
	void Add(FuncCharPtr* f)
	{
		FuncVec.push_back(f);
	}

	// Returns pointer to copy of current graphItem processed data
	GetResultStructure Get()
	{
		boost::mutex::scoped_lock lock(GraphItemMutex);
		char * dataCopy = new char[pointerToGetSize];
		memcpy (dataCopy,pointerToGet,pointerToGetSize);
		lock.unlock();
		GraphItemMutexConditionVariable.notify_one();
		GetResultStructure result;
		result.ptr = dataCopy;
		result.length = pointerToGetSize;
		return result;
	}

	void Clean()
	{
		GraphWorker.interrupt();
		GraphWorker.join();
		CleanAPI();
		//delete[] pointerToGet;
		//pointerToGet = 0;

	}

	// Cast data to subscribers and clean up given pointer
	void CastData(){
		for (size_t i = 0 ; i < FuncVec.size() ; i++){
			char * dataCopy = new char[pointerToGetSize];
			memcpy (dataCopy,pointerToGet,pointerToGetSize);
			FuncVec[i] (dataCopy, pointerToGetSize) ;}
	}

	// Cast given data to subscribers and clean up given pointer
	void CastData(char * data, int length){
		for(size_t i = 0 ; i < FuncVec.size(); i++){
			char* dataCopy = new char[length];
			memcpy(dataCopy, data, length);
			FuncVec[i](dataCopy, length);
		}
	}

private:
	// Char pointer to hold a copy of pointer of data we want to return on Get() call
	char* pointerToGet;
	int pointerToGetSize;

	// Vector to hold subscribed functions
	vector<FuncCharPtr*> FuncVec ;

};
#endif // _IGraphElement_h_