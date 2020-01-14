// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ortools/constraint_solver/search_limit.proto

#ifndef PROTOBUF_ortools_2fconstraint_5fsolver_2fsearch_5flimit_2eproto__INCLUDED
#define PROTOBUF_ortools_2fconstraint_5fsolver_2fsearch_5flimit_2eproto__INCLUDED

#include <string>

#include <google/protobuf/stubs/common.h>

#if GOOGLE_PROTOBUF_VERSION < 3002000
#error This file was generated by a newer version of protoc which is
#error incompatible with your Protocol Buffer headers.  Please update
#error your headers.
#endif
#if 3002000 < GOOGLE_PROTOBUF_MIN_PROTOC_VERSION
#error This file was generated by an older version of protoc which is
#error incompatible with your Protocol Buffer headers.  Please
#error regenerate this file with a newer version of protoc.
#endif

#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/arena.h>
#include <google/protobuf/arenastring.h>
#include <google/protobuf/generated_message_util.h>
#include <google/protobuf/metadata.h>
#include <google/protobuf/message.h>
#include <google/protobuf/repeated_field.h>  // IWYU pragma: export
#include <google/protobuf/extension_set.h>  // IWYU pragma: export
#include <google/protobuf/unknown_field_set.h>
// @@protoc_insertion_point(includes)
namespace operations_research {
class SearchLimitParameters;
class SearchLimitParametersDefaultTypeInternal;
extern SearchLimitParametersDefaultTypeInternal _SearchLimitParameters_default_instance_;
}  // namespace operations_research

namespace operations_research {

namespace protobuf_ortools_2fconstraint_5fsolver_2fsearch_5flimit_2eproto {
// Internal implementation detail -- do not call these.
struct TableStruct {
  static const ::google::protobuf::uint32 offsets[];
  static void InitDefaultsImpl();
  static void Shutdown();
};
void AddDescriptors();
void InitDefaults();
}  // namespace protobuf_ortools_2fconstraint_5fsolver_2fsearch_5flimit_2eproto

// ===================================================================

class SearchLimitParameters : public ::google::protobuf::Message /* @@protoc_insertion_point(class_definition:operations_research.SearchLimitParameters) */ {
 public:
  SearchLimitParameters();
  virtual ~SearchLimitParameters();

  SearchLimitParameters(const SearchLimitParameters& from);

  inline SearchLimitParameters& operator=(const SearchLimitParameters& from) {
    CopyFrom(from);
    return *this;
  }

  static const ::google::protobuf::Descriptor* descriptor();
  static const SearchLimitParameters& default_instance();

  static inline const SearchLimitParameters* internal_default_instance() {
    return reinterpret_cast<const SearchLimitParameters*>(
               &_SearchLimitParameters_default_instance_);
  }

  void Swap(SearchLimitParameters* other);

  // implements Message ----------------------------------------------

  inline SearchLimitParameters* New() const PROTOBUF_FINAL { return New(NULL); }

  SearchLimitParameters* New(::google::protobuf::Arena* arena) const PROTOBUF_FINAL;
  void CopyFrom(const ::google::protobuf::Message& from) PROTOBUF_FINAL;
  void MergeFrom(const ::google::protobuf::Message& from) PROTOBUF_FINAL;
  void CopyFrom(const SearchLimitParameters& from);
  void MergeFrom(const SearchLimitParameters& from);
  void Clear() PROTOBUF_FINAL;
  bool IsInitialized() const PROTOBUF_FINAL;

  size_t ByteSizeLong() const PROTOBUF_FINAL;
  bool MergePartialFromCodedStream(
      ::google::protobuf::io::CodedInputStream* input) PROTOBUF_FINAL;
  void SerializeWithCachedSizes(
      ::google::protobuf::io::CodedOutputStream* output) const PROTOBUF_FINAL;
  ::google::protobuf::uint8* InternalSerializeWithCachedSizesToArray(
      bool deterministic, ::google::protobuf::uint8* target) const PROTOBUF_FINAL;
  ::google::protobuf::uint8* SerializeWithCachedSizesToArray(::google::protobuf::uint8* output)
      const PROTOBUF_FINAL {
    return InternalSerializeWithCachedSizesToArray(
        ::google::protobuf::io::CodedOutputStream::IsDefaultSerializationDeterministic(), output);
  }
  int GetCachedSize() const PROTOBUF_FINAL { return _cached_size_; }
  private:
  void SharedCtor();
  void SharedDtor();
  void SetCachedSize(int size) const PROTOBUF_FINAL;
  void InternalSwap(SearchLimitParameters* other);
  private:
  inline ::google::protobuf::Arena* GetArenaNoVirtual() const {
    return NULL;
  }
  inline void* MaybeArenaPtr() const {
    return NULL;
  }
  public:

  ::google::protobuf::Metadata GetMetadata() const PROTOBUF_FINAL;

  // nested types ----------------------------------------------------

  // accessors -------------------------------------------------------

  // int64 time = 1;
  void clear_time();
  static const int kTimeFieldNumber = 1;
  ::google::protobuf::int64 time() const;
  void set_time(::google::protobuf::int64 value);

  // int64 branches = 2;
  void clear_branches();
  static const int kBranchesFieldNumber = 2;
  ::google::protobuf::int64 branches() const;
  void set_branches(::google::protobuf::int64 value);

  // int64 failures = 3;
  void clear_failures();
  static const int kFailuresFieldNumber = 3;
  ::google::protobuf::int64 failures() const;
  void set_failures(::google::protobuf::int64 value);

  // int64 solutions = 4;
  void clear_solutions();
  static const int kSolutionsFieldNumber = 4;
  ::google::protobuf::int64 solutions() const;
  void set_solutions(::google::protobuf::int64 value);

  // bool smart_time_check = 5;
  void clear_smart_time_check();
  static const int kSmartTimeCheckFieldNumber = 5;
  bool smart_time_check() const;
  void set_smart_time_check(bool value);

  // bool cumulative = 6;
  void clear_cumulative();
  static const int kCumulativeFieldNumber = 6;
  bool cumulative() const;
  void set_cumulative(bool value);

  // @@protoc_insertion_point(class_scope:operations_research.SearchLimitParameters)
 private:

  ::google::protobuf::internal::InternalMetadataWithArena _internal_metadata_;
  ::google::protobuf::int64 time_;
  ::google::protobuf::int64 branches_;
  ::google::protobuf::int64 failures_;
  ::google::protobuf::int64 solutions_;
  bool smart_time_check_;
  bool cumulative_;
  mutable int _cached_size_;
  friend struct  protobuf_ortools_2fconstraint_5fsolver_2fsearch_5flimit_2eproto::TableStruct;
};
// ===================================================================


// ===================================================================

#if !PROTOBUF_INLINE_NOT_IN_HEADERS
// SearchLimitParameters

// int64 time = 1;
inline void SearchLimitParameters::clear_time() {
  time_ = GOOGLE_LONGLONG(0);
}
inline ::google::protobuf::int64 SearchLimitParameters::time() const {
  // @@protoc_insertion_point(field_get:operations_research.SearchLimitParameters.time)
  return time_;
}
inline void SearchLimitParameters::set_time(::google::protobuf::int64 value) {
  
  time_ = value;
  // @@protoc_insertion_point(field_set:operations_research.SearchLimitParameters.time)
}

// int64 branches = 2;
inline void SearchLimitParameters::clear_branches() {
  branches_ = GOOGLE_LONGLONG(0);
}
inline ::google::protobuf::int64 SearchLimitParameters::branches() const {
  // @@protoc_insertion_point(field_get:operations_research.SearchLimitParameters.branches)
  return branches_;
}
inline void SearchLimitParameters::set_branches(::google::protobuf::int64 value) {
  
  branches_ = value;
  // @@protoc_insertion_point(field_set:operations_research.SearchLimitParameters.branches)
}

// int64 failures = 3;
inline void SearchLimitParameters::clear_failures() {
  failures_ = GOOGLE_LONGLONG(0);
}
inline ::google::protobuf::int64 SearchLimitParameters::failures() const {
  // @@protoc_insertion_point(field_get:operations_research.SearchLimitParameters.failures)
  return failures_;
}
inline void SearchLimitParameters::set_failures(::google::protobuf::int64 value) {
  
  failures_ = value;
  // @@protoc_insertion_point(field_set:operations_research.SearchLimitParameters.failures)
}

// int64 solutions = 4;
inline void SearchLimitParameters::clear_solutions() {
  solutions_ = GOOGLE_LONGLONG(0);
}
inline ::google::protobuf::int64 SearchLimitParameters::solutions() const {
  // @@protoc_insertion_point(field_get:operations_research.SearchLimitParameters.solutions)
  return solutions_;
}
inline void SearchLimitParameters::set_solutions(::google::protobuf::int64 value) {
  
  solutions_ = value;
  // @@protoc_insertion_point(field_set:operations_research.SearchLimitParameters.solutions)
}

// bool smart_time_check = 5;
inline void SearchLimitParameters::clear_smart_time_check() {
  smart_time_check_ = false;
}
inline bool SearchLimitParameters::smart_time_check() const {
  // @@protoc_insertion_point(field_get:operations_research.SearchLimitParameters.smart_time_check)
  return smart_time_check_;
}
inline void SearchLimitParameters::set_smart_time_check(bool value) {
  
  smart_time_check_ = value;
  // @@protoc_insertion_point(field_set:operations_research.SearchLimitParameters.smart_time_check)
}

// bool cumulative = 6;
inline void SearchLimitParameters::clear_cumulative() {
  cumulative_ = false;
}
inline bool SearchLimitParameters::cumulative() const {
  // @@protoc_insertion_point(field_get:operations_research.SearchLimitParameters.cumulative)
  return cumulative_;
}
inline void SearchLimitParameters::set_cumulative(bool value) {
  
  cumulative_ = value;
  // @@protoc_insertion_point(field_set:operations_research.SearchLimitParameters.cumulative)
}

#endif  // !PROTOBUF_INLINE_NOT_IN_HEADERS

// @@protoc_insertion_point(namespace_scope)


}  // namespace operations_research

// @@protoc_insertion_point(global_scope)

#endif  // PROTOBUF_ortools_2fconstraint_5fsolver_2fsearch_5flimit_2eproto__INCLUDED
